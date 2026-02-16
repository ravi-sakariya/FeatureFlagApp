using Azure.Core;
using FeatureFlag.Domain.Entity;
using FeatureFlag.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FeatureFlagsController : ControllerBase
{
    private readonly IFeatureFlagService _featureFlags;

    public FeatureFlagsController(IFeatureFlagService featureFlags)
    {
        _featureFlags = featureFlags;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var flags = await _featureFlags.GetAll();

        return Ok(flags);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var flag = await _featureFlags.GetByName(name);

        if (flag == null) return NotFound($"Feature flag '{name}' not found.");

        return Ok(flag);
    }

    [HttpGet("evaluate")]
    public async Task<IActionResult> Evaluate([FromQuery] string flag, [FromQuery] string userId, [FromQuery] string groups)
    {
        var userGroups = string.IsNullOrEmpty(groups)
            ? new List<string>()
            : groups.Split(',').ToList();

        var enabled = await _featureFlags.IsEnabled(flag, userId, userGroups);

        return Ok(new { Flag = flag, UserId = userId, Groups = userGroups, Enabled = enabled });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FeatureFlag.Domain.Entity.FeatureFlag request)
    {
        if (await _featureFlags.GetByName(request.Name) != null)
            return Conflict($"Feature flag '{request.Name}' already exists.");

        await _featureFlags.Create(request);

        return CreatedAtAction(nameof(GetByName), new { name = request.Name }, request);
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, [FromBody] FeatureFlag.Domain.Entity.FeatureFlag request)
    {
        var flag = await _featureFlags.Update(request);

        if (flag == null) return NotFound($"Feature flag '{name}' not found.");

        return Ok(flag);
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var flag = await _featureFlags.GetByName(name);

        if (flag == null) return NotFound($"Feature flag '{name}' not found.");

        await _featureFlags.Delete(name);

        return NoContent();
    }
}
