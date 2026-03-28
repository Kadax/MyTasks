import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withHashLocation, withInMemoryScrolling } from '@angular/router';

import {MAT_DATE_LOCALE, MatNativeDateModule} from '@angular/material/core';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { credentialsInterceptor } from './credentialsInterceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes, withHashLocation()),
    //, withInMemoryScrolling({ anchorScrolling: 'enabled' })
    provideClientHydration(withEventReplay()),
    provideHttpClient(withInterceptors([credentialsInterceptor])),
    importProvidersFrom(MatNativeDateModule),
    { provide: MAT_DATE_LOCALE, useValue: 'ru-RU' }
  ]
};

