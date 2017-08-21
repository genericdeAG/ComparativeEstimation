import { Weighting } from './Weighting';
export class Voting {
    constructor(
        public VoterId: string,
        public Weightings: Weighting[]
    ) {  }
}