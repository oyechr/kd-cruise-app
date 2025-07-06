# KD Cruise Ship App

## Description
KD Cruise Ship App is a proof-of-concept web application designed for managing planned events and live ship positions for luxury cruise ships. The project includes:
-	A PostgreSQL database for storing planned events.
-	Kubernetes-based deployment using Helm charts.
-	GitOps automation with ArgoCD.
-	Docker Compose setup for local development.

This POC is designed to test the feasibility of the application on one cruise ship before scaling to the entire fleet.


## Features 

- **Planned Events Management**: Store and retrieve planned events for voyages.
- **PostgreSQL Database**: Robust database for event storage.
- **Kubernetes Deployment**: Helm charts for managing Kubernetes resources.
- **Monitoring**: Prometheus and Grafana for observability.
- **CI/CD**: Automated build and deployment with GitHub Actions.



## Project Structure 
````
kd-cruise/
├── .github/
│   └── workflows/                      # CI/CD pipelines (GitHub Actions)
├── charts/                             # Helm charts for Kubernetes management
│   ├── kd-cruise-/                     # Parent (umbrella) Helm chart for the full stack
│   │   ├── Chart.yaml
│   │   ├── values.yaml
│   │   └── charts/                     # Populated by `helm dependency update`
│   ├── planned-events-api/            # Helm chart for the ASP.NET Core API
│   │   ├── Chart.yaml
│   │   ├── values.yaml
│   │   └── templates/
│   └── postgresql/                    # Helm chart for PostgreSQL
│       ├── Chart.yaml
│       ├── values.yaml
│       └── templates/
├── infrastructure/                     # ArgoCD manifests for GitOps-based deployments
│   └── argocd/
│       └── kd-postgres-application.yaml
├── src/                                # Application source code
│   ├── KD.Cruise.PlannedEventsApi/     # ASP.NET Core backend
│   └── KD.Cruise.BlazorApp/            # Frontend - Blazor WebAssembly
├── tests/                              # Unit & integration tests
│   └── KD.Cruise.Tests/
├── Makefile                            # Task automation for Helm/Kubernetes/dev tools
└── README.md                           # Project documentation and usage instructions
````

## Setup Instructions

### Prerequisites

Before you begin, ensure you have the following tools installed and set up on your local machine:

- **Git**: For cloning the repository.
- **Docker**: For building and running container images.
- **Kubernetes**: A working Kubernetes cluster (e.g., Minikube, GKE, EKS, AKS, etc.).
- **Helm**: For managing Kubernetes applications.
- **kubectl**: The Kubernetes command-line tool for interacting with your cluster.
- **psql**: A PostgreSQL client to interact with the database (can also be used via `kubectl exec`).
- **Make**: For running automated tasks via the provided `Makefile`.

### Clone the Repository

To get started, clone the repository and navigate into the project directory:

```bash
git clone https://github.com/oyechr/kd-cruise.git
cd kd-cruise
```



### Local Development and Deployment

You can use the provided `Makefile` to automate common tasks such as installing dependencies, deploying to Kubernetes, and managing Prometheus/Grafana:


### CI/CD with GitHub Actions

This repository uses GitHub Actions for automated builds and deployments:

- **Build and Push Docker Image**:  
  On every push to `main` (or PR), the API container image is built and pushed to the configured container registry. The package is public. 

> **Note:**
> The "Deploy to Local Minikube" GitHub Action runs in a cloud-based Minikube environment for CI/CD validation.  
> For local development, you can use the same commands (see the Makefile) to deploy the stack to your own Minikube or Kubernetes cluster.

- **Deploy to Local Minikube**:  
  On every push to `main` (or PR) that affects charts or monitoring, or after a successful image build, the stack is deployed to a local Minikube cluster using Helm.  
  This workflow:
  - Installs Prometheus and Grafana in the `monitoring` namespace.
  - Installs the API and PostgreSQL in the `kd-cruise` namespace.
  - Verifies deployments and services.

### Monitoring

- **Prometheus and Grafana** are deployed via Helm using custom values from `monitoring/values.yaml`.
- The API exposes metrics at `/metrics` for Prometheus scraping.
- Kubernetes Service and Deployment annotations are set for Prometheus discovery.


## Decisions Taken





## Planned Improvements



## License

This project is licensed under the MIT License. See the `LICENSE` file for details.