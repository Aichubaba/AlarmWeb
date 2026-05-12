#!/bin/bash
set -e
if [ -f /tmp/init.sql.gz ]; then
    echo "Restoring database from dump..."
    gunzip -c /tmp/init.sql.gz | psql -U "$POSTGRES_USER" -d "$POSTGRES_DB"
    echo "Restore completed."
fi