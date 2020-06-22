<#
PowerShell script to Create new project based on ISDCore project: 
* Copying project folder to folder with new project name
* Renaming .csproj file and other files with project name
* Changing project name reference in .sln solution file
* Changing RootNamespace and AssemblyName in .csproj file
* Renaming project inside AssemblyInfo.cs
* Change other files that has reference to ISDCore
---
*** Execute this command (Set-ExecutionPolicy Unrestricted) to run this script ***
---
------------------------
EXAMPLE PARAMETERS
ISDCoreFilePath: D:\SOURCE_CODE\ISDCore
NewProjectName: EmployeePortal
NewProjectFilePath: D:\Projects\Sandbox
------------------------
#>
param(
    [parameter(
        HelpMessage="ISDCore path",
        Mandatory=$true, ValueFromPipeline=$true)]
    [ValidateScript({
        if (Test-Path $_.Trim().Trim('"')) { $true }
        else { throw "Path does not exist: $_" }
    })]
    [string]$ISDCoreFilePath,
    
    [parameter(
        HelpMessage="New project file name, without extension.",
        Mandatory=$true)]
    [string]$NewProjectName,
 
    [parameter(
        HelpMessage="New project path.",
        Mandatory=$true)]
    [ValidateScript({
        if (Test-Path $_.Trim().Trim('"')) { $true }
        else { throw "Path does not exist: $_" }
    })]
    [string]$NewProjectFilePath
)

$ISDCoreFilePath = $ISDCoreFilePath.Trim().Trim('"')
$NewProjectFilePath = $NewProjectFilePath.Trim().Trim('"')

Write-Host ""
Write-Host "Creating '$NewProjectName' solution/project based on ISDCore" -ForegroundColor Magenta
Write-Host "============================================================" -ForegroundColor Yellow
Write-Host ""

function ProceedOrExit {
    if ($?) { 
        Write-Host "   Done" -ForegroundColor Green
    } 
    else { 
        Write-Host "Script FAILED! Exiting.." -ForegroundColor Red ; 
        exit 1 
    } 
}

Write-Host "1. Set current location to project folder '$ISDCoreFilePath'" -ForegroundColor Cyan
cd $ISDCoreFilePath 
ProceedOrExit
        
$NewProjectFolder="$NewProjectFilePath\$NewProjectName"
Write-Host "----------" -ForegroundColor Yellow
Write-Host "2. Copy project folder to '$NewProjectFolder'" -ForegroundColor Cyan
copy . $NewProjectFolder -Recurse
ProceedOrExit

$NewProjectFolder=Resolve-Path $NewProjectFolder
Write-Host "----------" -ForegroundColor Yellow
Write-Host "3. Set current location to New project folder '$NewProjectFolder'" -ForegroundColor Cyan
cd $NewProjectFolder
ProceedOrExit

Write-Host "----------" -ForegroundColor Yellow
Write-Host "4. Rename .proj and other files inside '$NewProjectFolder'" -ForegroundColor Cyan
$OldProjectName=[IO.Path]::GetFileNameWithoutExtension($ISDCoreFilePath)
Write-Host "   Source project name is '$OldProjectName'" -ForegroundColor Cyan
get-childItem -Filter "*$OldProjectName*" -Recurse | rename-item -NewName {$_.name -replace $OldProjectName, $NewProjectName }
dir -Hidden -Recurse | rename-item -NewName {$_.Name -replace $OldProjectName, $NewProjectName}
ProceedOrExit 

$BackupFolder="$NewProjectFolder.backup"
Write-Host "----------" -ForegroundColor Yellow
Write-Host "5. Copy solution file to '$BackupFolder'" -ForegroundColor Cyan
Copy . $BackupFolder -Recurse
ProceedOrExit
 
Write-Host "----------" -ForegroundColor Yellow
Write-Host "6. Update Solution file, Project file and AssemblyInfo in $NewProjectFolder\$NewProjectName" -ForegroundColor Cyan
Write-Host "   Update Project references and other files (.cs, .cshtml, .asax)" -ForegroundColor Cyan
$files = Get-ChildItem -Path $NewProjectFolder -Recurse -Include *.sln,*.csproj,*.cs,Global.asax,*.cshtml -Exclude TemporaryGeneratedFile*.*
for ($i=0; $i -lt $files.Count; $i++) {
    Write-Host "   "$files[$i].FullName -ForegroundColor DarkCyan
    (Get-Content -path $files[$i].FullName).replace($OldProjectName, $NewProjectName) | 
        Set-Content -path $files[$i].FullName
}
ProceedOrExit

$svnFolder = "$NewProjectFolder\.svn"
Write-Host "----------" -ForegroundColor Yellow
Write-Host "7. Remove all Source Control bindings" -ForegroundColor Cyan
remove-item –path $svnFolder –recurse -force
remove-item –path "$BackupFolder\.svn" –recurse -force
ProceedOrExit

Write-Host "============================================================" -ForegroundColor Yellow
Write-Host "'$NewProjectName' has been created!" -ForegroundColor Green