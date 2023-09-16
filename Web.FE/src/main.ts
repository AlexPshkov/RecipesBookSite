import {enableProdMode} from '@angular/core';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';

import {AppModule} from './app/app.module';
import {environment} from './environments/environment';

const module = platformBrowserDynamic().bootstrapModule(AppModule);

module.catch(error => console.warn(error))

if (environment.production) {
  enableProdMode();
}


