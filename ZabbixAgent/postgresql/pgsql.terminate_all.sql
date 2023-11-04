SELECT pg_terminate_backend(pid)
FROM pg_stat_activity
WHERE state='active' AND datname=':dbname' AND extract('epoch' FROM (clock_timestamp() - query_start)) > :tmax;