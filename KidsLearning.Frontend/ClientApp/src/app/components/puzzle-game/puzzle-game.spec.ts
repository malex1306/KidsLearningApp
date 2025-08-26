import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PuzzleGame } from './puzzle-game';

describe('PuzzleGame', () => {
  let component: PuzzleGame;
  let fixture: ComponentFixture<PuzzleGame>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PuzzleGame]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PuzzleGame);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
