import { Observable } from 'rxjs';
import { map, publishBehavior, refCount } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { User as Auth0User} from '@auth0/auth0-spa-js';

import { isNil } from '@movington/helpers';
import { User } from './user-details';

@Injectable({
  providedIn: 'root'
})
export class UserContext {
  constructor(private readonly authService: AuthService) {
    this.isAuthenticated$ = authService.isAuthenticated$;

    this.currentUser$ = authService.user$.pipe(
      map(x => this.mapUser(x)),
      publishBehavior<User | null>(null),
      refCount(),
    );
  }

  public readonly isAuthenticated$: Observable<boolean>;
  public readonly currentUser$: Observable<User | null>;

  private mapUser(user: Auth0User | undefined | null): User | null {
    if (isNil(user)) {
      return null;
    }

    return {
      email: user.email,
      username: user.name
    }
  }
}
