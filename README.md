# 🚢 KD Cruise Ship App

## 🧭 Overview
KD Cruise Ship App is a proof-of-concept web application designed for managing planned events and live ship positions for luxury cruise ships. The project includes:
-	A PostgreSQL database for storing planned events.
-	Kubernetes-based deployment using Helm charts.
-	GitOps automation with ArgoCD.
-	Docker Compose setup for local development.

This POC is designed to test the feasibility of the application on one cruise ship before scaling to the entire fleet.


## ✨ Key Features (Current)

- 📅 **Planned Events Management**: Store and retrieve planned events for voyages.
- 🗄️ **PostgreSQL Database**: Robust database for event storage.
- 🚀 **Kubernetes Deployment**: Helm charts for managing Kubernetes resources.
- 📊 **Monitoring**: Prometheus and Grafana for observability.
- ⚙️ **CI/CD**: Automated build and deployment with GitHub Actions.
  
> **Coming Soon:**
> - 🎨 Blazor frontend (scaffolded, development not started)
> - 🔁 GitOps with ArgoCD (infrastructure placeholders only)



## 📁 Project Structure 
````
kd-cruise/
├── .github/
│   └── workflows/                      # CI/CD pipelines (GitHub Actions)
├── charts/                             # Helm charts for Kubernetes management
│   ├── kd-cruise-/                     # Parent (umbrella) Helm chart for the full stack
│   │   ├── Chart.yaml
│   │   ├── values.yaml
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
├── monitoring/                         # Monitoring configs and custom Prometheus values
│   └── prometheus-values.yaml          # Custom values for Prometheus stack      └── kd-postgres-application.yaml
├── src/                                # Application source code
│   ├── KD.Cruise.PlannedEventsApi/     # ASP.NET Core backend
│   └── KD.Cruise.BlazorApp/            # Frontend - Blazor WebAssembly
├── tests/                              # Unit & integration tests
│   └── KD.Cruise.Tests/
├── Makefile                            # Task automation for Helm/Kubernetes/dev tools
└── README.md                           # Project documentation and usage instructions
````

## ⚙️ Setup Guide

### 🧰 Prerequisites

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


### 🧪 Local Development & Testing

The following steps have been tested and verified for local development:

1. **Start Minikube**  
   (using a named profile for isolation):
	`` minikube start -p kd-dev``
   
2. **Deploy the stack using the Makefile**  
   (run in Git Bash, WSL, or a compatible shell from the project root):
   `make dev`
	
3. **Verify deployments and services:**
	``kubectl get pods -A kubectl get svc -A``
	
4. **Access the API via Minikube service:**
   ``minikube service planned-events-api``
	
   This will open the Swagger UI in your browser.

5. **Test the API:**
	   - Use the Swagger UI to call the `/v1/events` endpoint and confirm it returns planned events.
	   - Test the `/healthz` endpoint to verify the API health.

> **Note:**  
> The Makefile automates the deployment of Prometheus, Grafana, the API, and PostgreSQL.  
> All steps were performed and verified in a local Minikube environment with the profile `kd-dev`


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