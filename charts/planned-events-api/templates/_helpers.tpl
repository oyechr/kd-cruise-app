{{- define "planned-events-api.name" -}}
{{- .Chart.Name | trunc 63 | trimSuffix "-" -}}
{{- end }}

{{- define "planned-events-api.fullname" -}}
{{- if .Values.fullnameOverride }}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- printf "%s" (include "planned-events-api.name" .) | trunc 63 | trimSuffix "-" }}
{{- end }}
{{- end }}