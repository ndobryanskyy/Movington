import { Observable, ReplaySubject } from 'rxjs';
import { Injectable, OnDestroy } from '@angular/core';

@Injectable()
export class DestroyToken extends Observable<void> implements OnDestroy {
  private readonly destroySubject: ReplaySubject<void>;

  constructor() {
    const replaySubject = new ReplaySubject<void>(1);

    super(x => replaySubject.subscribe(x));

    this.destroySubject = replaySubject;
  }

  public ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }
}
