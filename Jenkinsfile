pipeline{
    agent any
    
    stages{
        stage ('Clean WorkSpace Directory'){
            steps {
                cleanWs()
            }
        }
        stage ('Git CheckOut'){
            steps {
                git branch: 'feature/newversion', url: 'https://github.com/rahulrathore44/RestSharpFramework.git'    
            }
            
        }
        stage ('Restore Packages'){
            steps {
                bat 'C:\\Data\\JenkinsWar\\nuget.exe restore WebServiceAutomation.sln'
            }
        }
        stage ('Build'){
            steps {
                
                bat "\"${tool 'MSBuild'}\" -verbosity:detailed WebServiceAutomation.sln /p:Configuration=Release /p:Platform=\"Any CPU\""
            }
        }
        stage ('Deploy'){
            steps {
                bat 'echo Deploying the application..'
            }
        }
        stage ('Run the Test') {
            steps {
                bat "\"${tool 'VSTest'}\" WebServiceAutomation/bin/Release/WebServiceAutomation.dll RestSharpAutomation/bin/Release/RestSharpAutomation.dll MsTestProject/bin/Release/MsTestProject.dll /InIsolation /Logger:html"
            }
        }
    }
    
    post {
        always{
            archiveArtifacts artifacts: 'TestResults/**/*.html', fingerprint: true
            publishHTML([allowMissing: false, alwaysLinkToLastBuild: false, keepAll: false, reportDir: 'TestResults', reportFiles: 'index.html', reportName: 'HTML Report', reportTitles: ''])
        }
    }
}