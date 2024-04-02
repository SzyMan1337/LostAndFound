# Introduciton

This is a node module intended to contain all shared functionalities, classes etc. between web app and mobile app.

# Compilation

To compile use `npx tsc` or simply `tsc` commands in terminal in modules main directory.

# Installation

This module is intended only for use as local module. 
To install it into existing project you can `npm install --save [relative path to directory]` eg. `npm install --save ../commons`.
This will create symlink in /node_modules to modules directory.
You should be able to use is as normal module: `import { testType } from "commons";`.
