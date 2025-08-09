import { Question } from "./question";

export interface LearningTask {
    id: number;
    title: string;
    description: string;
    subject: string;
    questions: Question[];
}
