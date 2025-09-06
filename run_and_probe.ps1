$ErrorActionPreference = 'Stop'
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:ASPNETCORE_URLS = 'http://localhost:5078'

$stdout = 'run.stdout.log'
$stderr = 'run.stderr.log'
if (Test-Path $stdout) { Remove-Item $stdout -Force }
if (Test-Path $stderr) { Remove-Item $stderr -Force }

$proc = Start-Process -FilePath 'dotnet' -ArgumentList 'run --project WhatsAppChat.Api' -PassThru -WindowStyle Hidden -WorkingDirectory '.' -RedirectStandardOutput $stdout -RedirectStandardError $stderr

Start-Sleep -Seconds 2

$ok = $false
for ($i = 0; $i -lt 30; $i++) {
  try {
    $resp = Invoke-WebRequest 'http://localhost:5078/swagger/v1/swagger.json' -UseBasicParsing -TimeoutSec 2
    if ($resp.StatusCode -eq 200) { $ok = $true; break }
  } catch {
    Start-Sleep -Milliseconds 500
  }
}

$out = ''
if (Test-Path $stdout) { $out = Get-Content $stdout -Raw }
$err = ''
if (Test-Path $stderr) { $err = Get-Content $stderr -Raw }

Write-Output "===HTTP_OK===$ok"
Write-Output '===STDOUT==='; Write-Output $out
Write-Output '===STDERR==='; Write-Output $err

if ($proc -and -not $proc.HasExited) {
  Stop-Process -Id $proc.Id -Force
}