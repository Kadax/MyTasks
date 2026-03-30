import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { AppSettings, IEnvVars } from './app.settings';
import { enableProdMode } from '@angular/core';
import { environment } from './environments/environment';




fetch( environment.settingFile, {method: 'get'}).then((response) => {
    response.json().then((env_vars:IEnvVars) => {
        AppSettings.env_vars = env_vars;
        if (AppSettings.env_vars.ENV != 'development')
          enableProdMode();

        bootstrapApplication(App, appConfig)
          .catch((err) => console.error(err));

    });
});

