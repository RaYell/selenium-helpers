properties {
    $solution = '.\Selenium.WebDriver.Extensions.sln'
    $version = '2.0.1'
    $unitTests = '.\test\Selenium.WebDriver.Extensions.Tests\bin\Release\Selenium.WebDriver.Extensions.Tests.dll'
    $integrationTests = '.\test\Selenium.WebDriver.Extensions.IntegrationTests\bin\Release\Selenium.WebDriver.Extensions.IntegrationTests.dll'
    $coverageXml = '.\.artifacts\coverage.xml'
}

FormatTaskName '-------- {0} --------'

Task default -Depends Clean, Compile, Test, Coverage, Docs, Pack

Task CleanArtifacts -Description 'Cleans the artifacts directory' {
    $artifactsDir = '.\.artifacts'
    If (Test-Path $artifactsDir) {
        Remove-Item $artifactsDir -Recurse
    }
    New-Item -ItemType directory -Path $artifactsDir
}

Task CleanNet45 -Description 'Cleans the output directory of the default .NET 4.5 build configuration' {
    New-Build -Solution $solution -Target Clean
}

Task CleanNet40 -Description 'Cleans the output directory of the .NET 4.0 build configuration' {
    New-Build -Solution $solution -BuildConfiguration Release-Net40 -Target Clean
}

Task CleanNet35 -Description 'Cleans the output directory of the .NET 3.5 build configuration' {
    New-Build -Solution $solution -BuildConfiguration Release-Net35 -Target Clean
}

Task CleanDocs -Description 'Cleans the output directory of the documentation build configuration' {
    $envVarNotDefined = $env:SHFBROOT -eq $null
    If ($envVarNotDefined) {
        $env:SHFBROOT = $PWD.Path + '\packages\SHFB.2014.5.31\tools\Sandcastle Help File Builder'
    }
    Try {
        New-Build -Solution $solution -BuildConfiguration Docs -Target Clean
    } Finally {
        If ($envVarNotDefined) {
            $env:SHFBROOT = $null
        }
    }
}

Task Clean -Description 'Cleans the output directory of all build configurations' -Depends CleanNet45, CleanNet40, CleanNet35, CleanDocs, CleanArtifacts

Task CompileNet45 -Description 'Compiles the default .NET 4.5 build configuration' -Depends CleanNet45 {
    New-Build -Solution $solution
}

Task CompileNet40 -Description 'Compiles the .NET 4.0 build configuration' -Depends CleanNet40 {
    New-Build -Solution $solution -BuildConfiguration Release-Net40
}

Task CompileNet35 -Description 'Compiles the .NET 3.5 build configuration' -Depends CleanNet35 {
    New-Build -Solution $solution -BuildConfiguration Release-Net35
}

Task Compile -Description 'Compiles all of the build configurations' -Depends CompileNet45, CompileNet40, CompileNet35

Task Docs -Description 'Compiles the documentation build configuration' -Depends CleanDocs, CompileNet45 {
    $envVarNotDefined = $env:SHFBROOT -eq $null
    If ($envVarNotDefined) {
        $env:SHFBROOT = $PWD.Path + '\packages\SHFB.2014.5.31\tools\Sandcastle Help File Builder'
    }
    Try {
        New-Build -Solution $solution -BuildConfiguration Docs
    } Finally {
        If ($envVarNotDefined) {
            $env:SHFBROOT = $null
        }
    }
    
    Move-Item '.\docs\bin\Docs' '.\.artifacts'
}

Task Test -Description 'Runs the unit tests' -Depends CompileNet45 {
    Test-Assembly -Tests $unitTests
}

Task IntegrationPhantomJs -Description 'Runs the PhantomJS integration tests' -Depends CompileNet45 {
    Test-Assembly -Tests $integrationTests -Trait Browser=PhantomJS
}

Task IntegrationChrome -Description 'Runs the Chrome integration tests' -Depends CompileNet45 {
    Test-Assembly -Tests $integrationTests -Trait Browser=Chrome
}

Task IntegrationFirefox -Description 'Runs the Firefox integration tests' -Depends CompileNet45 {
    Test-Assembly -Tests $integrationTests -Trait Browser=Firefox
}

Task IntegrationInternetExplorer -Description 'Runs the Internet Explorer integration tests' -Depends CompileNet45 {
    Test-Assembly -Tests $integrationTests -Trait Browser=InternetExplorer
}

Task Integration -Description 'Runs all of the integration tests' -Depends IntegrationPhantomJs, IntegrationChrome, IntegrationFirefox, IntegrationInternetExplorer

Task AnalyzeCoverage -Description 'Analyzes the code coverage' -Depends CompileNet45 {
    New-CoverageAnalysis -Tests $unitTests -Output $coverageXml -Filter '+[Selenium.WebDriver.Extensions*]* -[*]*Exception* -[*Tests]* -[xunit*]*'
}

Task Coverage -Description 'Generates the code coverage HTML report' -Depends AnalyzeCoverage {
    New-CoverageReport -CoverageXml $coverageXml -Output .\.artifacts\CoverageReport
}

Task Coveralls -Description 'Sends coverage data to coveralls.io' -Depends AnalyzeCoverage {
    Publish-Coveralls -CoverageXml $coverageXml
}

Task Pack -Description 'Packs NuGet package' -Depends Compile {
    New-NugetPackage -Specification .\src\Selenium.WebDriver.Extensions\Selenium.WebDriver.Extensions.nuspec -Version $version
    Move-Item '.\*.nupkg' '.\.artifacts'
}
