#!/bin/bash
set -e
if [ -f /tmp/init.sql.gz ]; then
    echo "Restoring database from dump..."
    gunzip -c /tmp/init.sql.gz | psql -U "" -d ""
    echo "Restore completed."
fi