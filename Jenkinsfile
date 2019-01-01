pipeline {
  agent {
    docker {
      image 'node'
    }

  }
  stages {
    stage('Build') {
      parallel {
        stage('Build server') {
          steps {
            echo 'building'
          }
        }
        stage('Build client') {
          steps {
            echo 'Building client'
          }
        }
      }
    }
    stage('Test') {
      steps {
        echo 'Testing'
      }
    }
    stage('Deploy') {
      steps {
        input(message: 'Continue to deploy', ok: 'Yes')
      }
    }
  }
}