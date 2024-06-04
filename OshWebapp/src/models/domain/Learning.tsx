export interface Learning {
  id: number;
  name: string;
  text: string;
  learningSectionFile: LearningSectionFile;
}

export interface LearningSectionFile {
  id?: number;
  filePath?: string;
  fileType?: string;
}
