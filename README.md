#KD Cruise Ship App

##Description
KD Cruise Ship App is a proof-of-concept web application designed for managing planned events and live ship positions for luxury cruise ships. The project includes:
-	A PostgreSQL database for storing planned events.
-	Kubernetes-based deployment using Helm charts.
-	GitOps automation with ArgoCD.
-	Docker Compose setup for local development.

This POC is designed to test the feasibility of the application on one cruise ship before scaling to the entire fleet.


##Features 

- **Planned Events Management**: Store and retrieve planned events for voyages.
- **PostgreSQL Database**: Robust database for event storage.
- **Kubernetes Deployment**: Helm charts for managing Kubernetes resources.
- **GitOps Automation**: ArgoCD for continuous delivery and synchronization.
- **Adminer Integration**: Web-based database management tool for PostgreSQL.


##Project Structure 

├── .github/workflows/ # CI/CD pipeline (GitHub Actions)
├── charts/ # Helm charts for Kubernetes management
│   └── postgresql/
│       ├── templates/ # Helm templates for PostgreSQL deployment
│       │   ├── deployment.yaml # Deployment manifest
│       │   ├── service.yaml # Service manifest
│       │   ├── configmap.yaml # ConfigMap for database configuration
│       │   └── _helpers.tpl # Helper functions for Helm templates
│       ├── values.yaml # Helm values for PostgreSQL
│       └── Chart.yaml # Helm chart metadata
├── database/ # DB init and schema
│   └── init.sql # PostgreSQL initialization script
├── infrastructure/ # ArgoCD manifests for GitOps
│   └── argocd/
│       └── kd-postgres-application.yaml # ArgoCD application manifest
├── docker/ # Docker Compose files
│   └── docker-compose.yml
├── src/ # Application source code
│   ├── KD.Cruise.PlannedEventsApi/ # ASP.NET Core API
│   └── KD.Cruise.BlazorApp/ # Frontend - Blazor WebAssembly
├── tests/ # Unit/integration tests
│   └── KD.Cruise.Tests/
└── README.md

##Setup Instructions

###Clone Repo

```bash
git clone https://github.com/oyechr/kd-cruise.git
cd kd-cruise
```







## Decisions Taken

- **Helm for Kubernetes Management**: Chosen for its flexibility and reusability.
- **ArgoCD for GitOps**: Enables automated deployment and synchronization.
- **PostgreSQL**: Selected for its compatibility with time-series data and ease of integration.
- **Adminer**: Added for easy database management during development.



## Planned Improvements

- Add monitoring and alerting services for the Kubernetes cluster.
- Implement unit and integration tests for the API and frontend.
- Extend the database schema to include live ship position data.
- Optimize Helm charts for production readiness.



## License

This project is licensed under the MIT License. See the `LICENSE` file for details.