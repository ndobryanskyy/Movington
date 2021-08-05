import { isNil } from './utils';

export class BrowserContext {
  public static get origin(): string {
    if (isNil(window)) {
      return '';
    }

    return window.location.origin;
  }
}
