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
          agent {
            docker {
              image 'mhart/alpine-node'
            }

          }
          steps {
            echo 'building'
          }
        }
        stage('Build client') {
          agent {
            docker {
              image 'mhart/alpine-node'
            }

          }
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
        echo 'Deployed'
      }
    }
  }
}
