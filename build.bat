set mod=ModularilyBased
set modpath=%SUBNAUTICA_PATH%\BepInEx\plugins\%mod%
mkdir "%modpath%"
copy "bin\Release\net472\%mod%.dll" "%modpath%\%mod%.dll"
robocopy "Localization" "%modpath%\Localization" /E
robocopy "Assets" "%modpath%\Assets" /E
exit /B 0