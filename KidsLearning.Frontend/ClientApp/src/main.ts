import {bootstrapApplication} from '@angular/platform-browser';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {appConfig} from './app/app.config';
import {App} from './app/app';
import {AuthInterceptor} from './app/services/auth.interceptor';

const updatedAppConfig = {
  ...appConfig,
  providers: [
    ...(appConfig.providers || []),
    provideHttpClient(withInterceptors([AuthInterceptor]))
  ]
};

bootstrapApplication(App, updatedAppConfig)
  .catch((err) => console.error(err));
