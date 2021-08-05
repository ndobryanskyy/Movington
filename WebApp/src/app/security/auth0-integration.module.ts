import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthConfig, AuthHttpInterceptor, AuthModule } from '@auth0/auth0-angular';

import { BrowserContext } from '@movington/helpers';
import { environment } from '../../environments/environment';

const AUTH0_ENV = environment.auth0;

const config: AuthConfig = {
  clientId: AUTH0_ENV.clientId,
  domain: AUTH0_ENV.domain,
  audience: AUTH0_ENV.audience,
  redirectUri: BrowserContext.origin,
  httpInterceptor: {
    allowedList: [
      {
        uri: `${environment.apiBaseUrl}/*`
      }
    ]
  },
}

@NgModule({
  imports: [
    AuthModule.forRoot(config)
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: AuthHttpInterceptor
    }
  ],
  exports: [AuthModule],
})
export class Auth0IntegrationModule {
}
