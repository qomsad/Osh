export interface Training {
  id: number;
  questionType: string;
  rate: number;
  question: string;
  answers: Answer[];
}

export interface Answer {
  id?: number;
  index: number;
  value: string;
  isRight: boolean;
}
