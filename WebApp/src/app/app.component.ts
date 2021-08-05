import { Observable } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { Component, Self } from '@angular/core';

import { DestroyToken } from '@movington/helpers';
import { AuthenticationService } from './security/authentication-service';
import { UserContext } from './security/user-context';
import { User } from './security/user-details';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  viewProviders: [DestroyToken]
})
export class AppComponent {
  constructor(
    private readonly authenticationService: AuthenticationService,
    private readonly userContext: UserContext,
    @Self() private readonly destroy$: DestroyToken
  ) {
    this.authenticationReady$ = this.authenticationService.isReady$;

    this.showLoginButton$ = this.userContext.isAuthenticated$.pipe(
      map(isAuthenticated => !isAuthenticated)
    );
    this.showLogoutButton$ = this.userContext.isAuthenticated$;

    this.currentUser$ = this.userContext.currentUser$;
  }

  public readonly authenticationReady$: Observable<boolean>;

  public readonly showLoginButton$: Observable<boolean>;
  public readonly showLogoutButton$: Observable<boolean>;

  public readonly currentUser$: Observable<User | null>;

  public login(): void {
    this.authenticationService.login$().pipe(
      takeUntil(this.destroy$)
    ).subscribe();
  }

  public logout(): void {
    this.authenticationService.logout();
  }
}
