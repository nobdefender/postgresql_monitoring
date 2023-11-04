<h1 align="center">
  <br>
  <a href="https://github.com/nobdefender/postgresql_monitoring"><img src="https://raw.githubusercontent.com/nobdefender/postgresql_monitoring/main/logo.png" alt="PostgreSQL Monitoring"></a>
</h1>

<h4 align="center">A PostgreSQL Monitoring Tool integrated in Telegram Bot</h4>

<p align="center">
    <a href="https://github.com/nobdefender/postgresql_monitoring/commits">
    <img src="https://img.shields.io/github/last-commit/nobdefender/postgresql_monitoring.svg?style=flat-square&logo=github&logoColor=white"
         alt="GitHub last commit">
    <a href="https://github.com/nobdefender/postgresql_monitoring/issues">
    <img src="https://img.shields.io/github/issues-raw/nobdefender/postgresql_monitoring.svg?style=flat-square&logo=github&logoColor=white"
         alt="GitHub issues">
    <a href="https://github.com/nobdefender/postgresql_monitoring/pulls">
    <img src="https://img.shields.io/github/issues-pr-raw/nobdefender/postgresql_monitoring.svg?style=flat-square&logo=github&logoColor=white"
         alt="GitHub pull requests">
</p>
      
<p align="center">
  <a href="#installation">Installation</a> •
  <a href="#features">Features</a> •
  <a href="#wiki">Wiki</a> •
  <a href="#contributing">Contributing</a> •
  <a href="#support">Support</a> •
  <a href="#license">License</a>
</p>

---

<table>
<tr>
<td>
  
**PostgreSQL Monitoring Tool** is a monitoring tool integrated in the **Telegram bot**. <br />
This tool uses **Zabbix Server** on the monitoring tool server and **Zabbix Agent** on a machine with PostgreSQL deployed.

## Installation

##### Downloading and installing steps:

1. **[Download](https://www.zabbix.com/download)** and install Zabbix Server.
2. **[Download](https://www.zabbix.com/download_agents)** and install Zabbix Agent.
3. **[Download](https://github.com/nobdefender/postgresql_monitoring/archive/refs/tags/release.zip)** the latest version of the tool.
4. Extract archive with source code.
5. Run command in cmd: <br /> `docker compose -f ./docker-compose.yml up --build -d`

## Features

|                              | 🔰 PostgreSQL Monitoring Tool |
| ---------------------------- | :---------------------------: |
| Disk space monitoring        |              ✔️               |
| PostgreSQL uptime monitoring |              ✔️               |
| Restart PostgreSQL           |              ✔️               |
| Kill Slow Queries            |              ✔️               |
| Integrated Telegram Bot      |              ✔️               |
| Zabbix Support               |              ✔️               |
| Easy scalability             |              ✔️               |
| Easy to customize            |              ✔️               |
| high fault tolerance         |              ✔️               |
| Web admin service            |              ✔️               |

## Wiki

Do you **need some help**? Check out the _articles_ on the [wiki](https://github.com/nobdefender/postgresql_monitoring/wiki/).

## Contributing

Got **something interesting** you'd like to **share**? Learn about [contributing](https://docs.github.com/en/get-started/quickstart/contributing-to-projects).

## Support

Reach out to me via the **[profile addresses](https://github.com/nobdefender)**.

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-lightgrey.svg)](https://opensource.org/license/mit/)
