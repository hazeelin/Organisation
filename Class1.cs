[AllowAnonymous]
[HttpPost()]
public async Task<IActionResult> Post(LoginDTO model)
{
    if (!ModelState.IsValid) return BadRequest("errors.invalidParams");
    var user = await _userManager.FindByEmailAsync(model.Email);

if (user == null)
{
    return Unauthorized("errors.loginFailure");
}

var result = await _signInManager.PasswordSignInAsync(user?.UserName, model.Password, model.RememberMe, false);


if (result.Succeeded)
{
    var claims = new List<Claim>
        {
            new Claim(AppClaim.TenantId, user.TenantId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

    claims.AddRange(await _authOperations.GetUserRolesAsClaims(user));
    claims.AddRange(await _authOperations.GetAllUserClaims(user));

    var accessToken = _tokenService.GenerateAccessToken(claims);
    var refreshToken = _tokenService.GenerateRefreshToken();
    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

    await _ctx.SaveChangesAsync();

    return Ok(new TokenExchangeDTO
    {
        AccessToken = accessToken,
        RefreshToken = refreshToken
    });
}