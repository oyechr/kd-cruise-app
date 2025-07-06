.PHONY: all dev setup-helm-repos deps install uninstall clean status \
        install-prometheus uninstall-prometheus prometheus grafana

STACK_NAME = kd-cruise
CHART_PATH = charts/$(STACK_NAME)
PROM_STACK_NAME = prometheus
PROM_NAMESPACE = monitoring
PROM_VALUES = monitoring/values.yaml  

setup-helm-repos:
	@echo "🔗 Adding/updating Helm repos..."
	@helm repo list | grep -q prometheus-community || helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
	@helm repo update

deps: setup-helm-repos
	@echo "📦 Updating chart dependencies..."
	cd $(CHART_PATH) && helm dependency update

install-prometheus: setup-helm-repos
	@echo "📡 Installing Prometheus stack in '$(PROM_NAMESPACE)' namespace..."
	@helm upgrade --install $(PROM_STACK_NAME) prometheus-community/kube-prometheus-stack \
		-n $(PROM_NAMESPACE) --create-namespace \
		-f $(PROM_VALUES)

uninstall-prometheus:
	@echo "🧨 Uninstalling Prometheus stack from '$(PROM_NAMESPACE)'..."
	@helm uninstall $(PROM_STACK_NAME) -n $(PROM_NAMESPACE) || true

install: deps
	@echo "🚀 Installing the KD Cruise app stack..."
	@helm upgrade --install $(STACK_NAME) $(CHART_PATH) -n kd-cruise --create-namespace

uninstall:
	@echo "🧹 Uninstalling the KD Cruise app stack..."
	@helm uninstall $(STACK_NAME) -n kd-cruise || true

clean: uninstall uninstall-prometheus

status:
	@echo "📋 Cluster status:"
	kubectl get pods -A
	kubectl get svc -A

prometheus:
	@echo "📊 Opening Prometheus UI on localhost:9090..."
	kubectl -n $(PROM_NAMESPACE) port-forward svc/$(PROM_STACK_NAME)-kube-prometheus-stack-prometheus 9090:9090

grafana:
	@echo "📈 Opening Grafana UI on localhost:3000..."
	kubectl -n $(PROM_NAMESPACE) port-forward svc/$(PROM_STACK_NAME)-kube-prometheus-stack-grafana 3000:80


all: install-prometheus install
dev: all
