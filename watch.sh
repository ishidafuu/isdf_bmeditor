#!/bin/bash

export DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1
dotnet watch run --no-hot-reload
