#!/usr/bin/env bash

set -e

# ---- Input ----
SERVICE_NAME_INPUT="$1"

if [ -z "$SERVICE_NAME_INPUT" ]; then
  echo "Usage: ./create-service.sh <service_name>"
  exit 1
fi

# ---- Find first .sln file ----
SOLUTION_FILE=$(ls *.sln 2>/dev/null | head -n 1)

if [ -z "$SOLUTION_FILE" ]; then
  echo "No .sln file found in current directory"
  exit 1
fi

NAME=$(basename "$SOLUTION_FILE" .sln)

# ---- Convert snake_case to PascalCase ----
PASCAL_CASE=$(echo "$SERVICE_NAME_INPUT" \
  | awk -F_ '{ for (i=1;i<=NF;i++) printf toupper(substr($i,1,1)) substr($i,2) }')

SERVICE="${PASCAL_CASE}Service"
FOLDER=$(echo "$SERVICE_NAME_INPUT" | tr '[:upper:]' '[:lower:]')

# ---- Create ABP module ----
abp new "$NAME.$SERVICE" -t module --no-ui -o "services/$FOLDER"

# ---- Remove unwanted projects ----
find "services/$FOLDER" -type d \( \
  -name "*.IdentityServer" -o \
  -name "*.MongoDB.Tests" -o \
  -name "*.MongoDB" -o \
  -name "*.Host.Shared" -o \
  -name "*.Installer" \
\) -exec rm -rf {} +

# ---- Add projects to solution ----
find "services/$FOLDER" -name "*.csproj" -print0 \
  | xargs -0 dotnet sln "$NAME.sln" add
