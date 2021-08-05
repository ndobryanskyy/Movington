import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { BrowserContext } from '@movington/helpers';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private readonly authService: AuthService) {
    this.isReady$ = authService.isLoading$.pipe(
      map(isLoading => !isLoading),
    );
  }

  public readonly isReady$: Observable<boolean>;

  public login$(): Observable<void> {
    return this.authService.loginWithPopup();
  }

  public logout(): void {
    this.authService.logout({
      federated: true,
      returnTo: BrowserContext.origin
    });
  }
}
