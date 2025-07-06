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
- **GitOps Automation**: ArgoCD for continuous delivery and synchronization.



## Project Structure 
````
kd-cruise/
├── .github/
│   └── workflows/                      # CI/CD pipeline (GitHub Actions)
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
- **Kubernetes**: Make sure you have a working Kubernetes cluster (e.g., Minikube, GKE, EKS, AKS, etc.).
- **Helm**: Helm should be installed to manage Kubernetes applications.
- **kubectl**: The Kubernetes command-line tool for interacting with your cluster.
- **psql**: A PostgreSQL client to interact with the database (should also be available via `kubectl exec`).

### Clone the Repository

To get started, clone the repository and navigate into the project directory:

```bash
git clone https://github.com/oyechr/kd-cruise.git
cd kd-cruise
```


## PostgreSQL Setup with Helm and Kubernetes

















## Decisions Taken





## Planned Improvements



## License

This project is licensed under the MIT License. See the `LICENSE` file for details.