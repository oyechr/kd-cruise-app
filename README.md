# 🚢 KD Cruise Ship App

## 🧭 Overview
KD Cruise Ship App is a proof-of-concept web application designed for managing planned events and live ship positions for luxury cruise ships. The project includes:
-	A PostgreSQL database for storing planned events.
-	Kubernetes-based deployment using Helm charts.
-   API for planned events management (currently facing Prometheus target visibility issue).
-   Prometheus and Grafana for observability and monitoring of system metrics.


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

The following steps have been tested and verified for local development (`Commit 1d2e434`):

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

6. **Prometheus & Grafana Setup:**

    Both services confirmed to be running after deployment.

7. **Access Grafana Dashboard:**

    PostgreSQL metrics are fully functional and displayed in Grafana.
    API metrics row is pre-configured and will populate once Prometheus detects the API as a valid scrape target.

> **Note:**  
> The Makefile automates the deployment of Prometheus, Grafana, the API, and PostgreSQL.  
> The port forwading is not correctly handled by the Makefile, so you may need to port-forward manually as described below.
> All steps were performed and verified in a local Minikube environment with the profile `kd-dev`

### Accessing Grafana and Prometheus Dashboards Manually
If you prefer or need to port-forward manually (Makefile commands don’t work as expected), you can do so with the following steps:

1. Get Grafana Admin Password

If the default password (`prom-operator`) does not work or you need to retrieve the password, run the following command to decode the Grafana admin password:
For Linux/macOS or WSL:

`kubectl --namespace monitoring get secrets prometheus-grafana -o jsonpath="{.data.admin-password}" | base64 -d; echo`

For Windows PowerShell:

`kubectl -n monitoring get secret prometheus-grafana -o jsonpath="{.data.admin-password}" | % { [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($_)) }`

This will output the Grafana admin password.

2. Get Grafana Admin Username

You can also retrieve the username for Grafana if needed:

For Linux/macOS or WSL:

`kubectl --namespace monitoring get secrets prometheus-grafana -o jsonpath="{.data.admin-user}" | base64 -d; echo`

For Windows PowerShell:

`kubectl -n monitoring get secret prometheus-grafana -o jsonpath="{.data.admin-user}" | % { [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($_)) }`

3. Port-Forward Grafana

To access the Grafana dashboard, run the following command to manually port-forward Grafana to localhost:3000:

`kubectl -n monitoring port-forward svc/prometheus-kube-prometheus-stack-grafana 3000:80`

This will make Grafana accessible at http://localhost:3000 in your browser. When prompted, use the admin username and the password retrieved from the previous step (or use the default prom-operator if that works).

4. Port-Forward Prometheus

To access the Prometheus dashboard, run the following command to manually port-forward Prometheus to localhost:9090:

`kubectl -n monitoring port-forward svc/prometheus-kube-prometheus-prometheus 9090:9090`

This will make Prometheus accessible at http://localhost:9090 in your browser.

### ⚙️ CI/CD with GitHub Actions

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

### 📊 Monitoring

- **Prometheus and Grafana** are deployed via Helm using custom values from `monitoring/values.yaml`.
- The API exposes metrics at `/metrics` for Prometheus scraping.
- Kubernetes Service and Deployment annotations are set for Prometheus discovery.


##  🧠 Decisions Taken
This project was all about building a working Kubernetes-deployed app while also learning the nuts and bolts behind the scenes. 
Here's a quick breakdown of the decisions I made along the way, why I made them, and what worked (and didn’t).

### 🗃️ Database (PostgreSQL)

Made my own minimal Helm chart instead of using something like Bitnami. 
The idea was to get a better understanding of how the pieces fit together without being overwhelmed by boilerplate I didn’t understand.

### ⚙️ API (ASP.NET Core)

The API is built in .NET 9 using ASP.NET Core and Swagger/OpenAPI annotations. 
Familiar stack for me, and Swagger makes it super easy to test endpoints and hand off clean documentation to others. The annotations kept things tidy.


### ☸️ Deployment (Kubernetes + Helm)

Started small — wrote basic Helm charts for the API and DB to learn how things work. Later grouped them into a parent chart for easier management. 
For Prometheus, I used the community Helm chart to get Grafana and exporters included — less reinventing, more observing.

### 🔐 Namespaces

Prometheus runs in its own namespace, separate from the app. 
This is mostly to follow best practices — it keeps monitoring stuff isolated, makes RBAC cleaner, and avoids clashing with app resources.

### 🤖 GitHub Actions

Set up CI to build, tag, and push the API container to GHCR. 
It also runs a quick Kubernetes deployment test to make sure commands work outside my local setup. Keeps things clean and reproducible.


### 📈 Observability (Prometheus + Grafana)

Got Prometheus and Grafana running, metrics from PostgreSQL looked good. But… I hit a wall with getting API metrics to show up. 
Locally, the /metrics endpoint worked fine, logs looked clean, but Prometheus just wouldn’t see the target.
Tried debugging this for way too long, and it ended up stalling progress on other parts.

### 🧪 Testing / 

I scaffolded a test project early on, even though it was out of scope. Felt like a good investment for future dev and just good habit.

 ### 🖥️ UI (Blazor Web App – Razor Components)

Even though it was out of scope, a Blazor WebAssembly app was scaffolded early on as a placeholder for the future UI.

The decision to use Razor components was driven by a desire to stay within the C#/.NET ecosystem and ensure a cohesive full-stack development experience.

Keeping everything in the same language and framework:

   - Reduces cognitive overhead when switching between frontend and backend.

   -  Makes it easier to reuse DTOs, validation logic, and authentication strategies.

   -  Lays a clean foundation for building interactive dashboards or admin panels later using C# without jumping to JavaScript-heavy stacks.


### 🛠️ Developer Experience

Wrote a Makefile to speed up local setup and testing — didn’t want to live in the terminal too much.
Also figured it would be helpful for anyone else jumping in.

### 🛑 What Didn’t Go So Well

Got a bit stuck trying to fix the missing API metrics. Also got sidetracked trying to automate port-forwarding in the Makefile, and briefly dipped into Prometheus alerting/webhooks for Task 4. 
Decided to pull back and focus on finishing what I could properly instead of half-starting new problems.

## 🔭 Looking Ahead

Resolving Prometheus scrape target for the API.

Implementing Prometheus-based alerts and routing them via webhooks.

Wiring up the Blazor app to consume the planned events API.

Adding authentication via ASP.NET Identity or external providers.

Expanding monitoring to include logging via Loki or distributed tracing



## License

This project is licensed under the MIT License. See the `LICENSE` file for details.