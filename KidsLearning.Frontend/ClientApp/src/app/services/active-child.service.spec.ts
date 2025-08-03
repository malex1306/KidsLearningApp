import { TestBed } from '@angular/core/testing';

import { ActiveChildService } from './active-child.service';

describe('ActiveChildService', () => {
  let service: ActiveChildService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActiveChildService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
