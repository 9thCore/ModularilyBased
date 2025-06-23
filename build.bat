set mod=ModularilyBased
set modpath=%SUBNAUTICA_PATH%\BepInEx\plugins\%mod%
mkdir "%modpath%"
copy "bin\Release\net472\%mod%.dll" "%modpath%\%mod%.dll"
robocopy "Definitions" "%modpath%\Definitions" /E
exit /B 0