# MeidaAPI - `data.gov.il` Information API Aggregator

## Motivation

**Two Main Reasons:**

- `data.gov.il` is difficult to comprehend. It poses challenges in understanding and utilization.
- This project serves as a learning opportunity for C#, .NET, and backend development in general.

At first, this is mainly about learning through a project, and secondly about being an API aggregator. The scope might
change.

## Current Datasets

- Provident fund: https://data.gov.il/dataset/gemelnet

## Architecture

```
╔════════════════╗          ╔═══════════╗
║ Data Ingestor  ║  =====>  ║ MeidaAPI  ║
║────────────────║          ║───────────║
║ - Gets the Data║          ║ - an API  ║
║   from data.gov║          ║   that    ║
║   - Pushes it  ║          ║   pulls   ║
║   to MongoDB   ║          ║   from    ║
║                ║          ║   MongoDB ║
╚════════════════╝          ╚═══════════╝
```

The MeidaAPI has a better API IN MY PERSONAL OPINION. It also contains explanations to the different fields.

## TODO:

- Generalize it more, as currently, it is highly specific to Provident Funds only, and the code is not expandable or
  modular.
- Make it configurable (such as delay times for the ingestor, MongoDB usernames, and passwords, which are currently
  hard-coded, etc.).
- Develop a front end, preferably in Flutter. However, a frontend in JS will suffice as well (and can be a separate
  project-based learning).
- Automate the insertion of explanations about a given dataset. Many datasets have obscured titles, and there is an
  accompanying .xlsx file that explains what the different fields mean.

## License
The based Unlicense License.
