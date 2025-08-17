import { TestBed } from '@angular/core/testing';

import { Reward } from './reward';

describe('Reward', () => {
  let service: Reward;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Reward);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
