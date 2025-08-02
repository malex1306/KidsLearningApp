import { TestBed } from '@angular/core/testing';

import { ParentDashboard } from './parent-dashboard';

describe('ParentDashboard', () => {
  let service: ParentDashboard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParentDashboard);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
