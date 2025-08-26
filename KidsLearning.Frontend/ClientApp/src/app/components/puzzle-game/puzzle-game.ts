import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {QuizLogic, PuzzleTile} from '../../services/quiz-logic';
import {Router} from '@angular/router';

interface BoardField {
  id: number;
  correctPosition: number;
  tile: PuzzleTile | null;
}

@Component({
  selector: 'app-puzzle-game',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './puzzle-game.html',
  styleUrl: './puzzle-game.css',
})
export class PuzzleGame implements OnInit { // Stellen Sie sicher, dass der Klassenname 'PuzzleGameComponent' ist.
  imageUrl = 'assets/images/boy.png';
  rows = 3;
  cols = 3;
  tileSize = 150;
  boardFields: BoardField[] = [];
  availableTiles: PuzzleTile[] = [];
  draggedTile: PuzzleTile | null = null;
  isSolved = false;
  statusMessage: string = '';

  constructor(private quizLogic: QuizLogic, private router: Router) {
  }

  ngOnInit(): void {
    this.initializeBoard();
  }

  initializeBoard(): void {
    const totalTiles = this.rows * this.cols;
    const allTiles = this.quizLogic.createPuzzle(this.rows, this.cols, this.tileSize);

    this.boardFields = Array.from({length: totalTiles}, (_, i) => ({
      id: i,
      correctPosition: allTiles[i].correctPosition,
      tile: null,
    }));

    this.availableTiles = this.quizLogic.shuffleTiles(allTiles);
    this.statusMessage = '';
  }

  dragStart(event: DragEvent, tile: PuzzleTile): void {
    this.draggedTile = tile;
    event.dataTransfer?.setData('text/plain', tile.id.toString());
    this.statusMessage = '';
  }

  allowDrop(event: DragEvent): void {
    event.preventDefault();
  }

  drop(event: DragEvent, targetField: BoardField): void {
    event.preventDefault();

    if (!this.draggedTile) return;

    if (targetField.tile) {
      this.statusMessage = 'Dieses Feld ist bereits belegt.';
      this.draggedTile = null;
      return;
    }

    const sourceField = this.boardFields.find(field => field.tile?.id === this.draggedTile!.id);

    if (sourceField) {
// Wenn von einem Feld gezogen wird
      sourceField.tile = null;
    } else {
// Wenn von den verfügbaren Kacheln gezogen wird
      this.availableTiles = this.availableTiles.filter(t => t.id !== this.draggedTile!.id);
    }

    targetField.tile = this.draggedTile;

    this.checkCompletion();
    this.draggedTile = null;
  }

  checkCompletion(): void {
    const isSolved = this.boardFields.every(field => field.tile && field.tile.id === field.correctPosition);

    console.log('--- Puzzle-Status ---');
    this.boardFields.forEach(field => {
      console.log(`Feld-ID: ${field.id}, Korrekte Pos: ${field.correctPosition}, Kachel-ID: ${field.tile?.id ?? 'leer'}`);
    });
    console.log('Ist gelöst?', isSolved);
    console.log('---------------------');

    this.isSolved = isSolved;

    if (this.isSolved) {
      this.statusMessage = '🎉 Super! Du hast das Puzzle gelöst! 🎉';
      console.log('Puzzle gelöst!');
    } else {
      this.statusMessage = '';
    }
  }

  finishGame(): void {
    this.router.navigate(['/start-page']);
  }
}
