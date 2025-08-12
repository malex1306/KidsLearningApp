export interface Question {
    id: number;
    text: string;
    correctAnswer: string;
    options: string[];
    imageUrl?: string;
}