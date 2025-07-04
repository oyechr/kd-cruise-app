{{- define "postgresql.name" -}}
postgresql
{{- end -}}

{{- define "postgresql.fullname" -}}
{{ .Release.Name }}-{{ include "postgresql.name" . }}
{{- end -}}
