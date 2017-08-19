import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'sprint-creation',
    templateUrl: './sprint-creation.component.html',
    styleUrls: ['./sprint-creation.component.css']
})
export class SprintCreationComponent {
    emptyStory = "";
    
    stories: string[] = [];
    isAddStoryEnabled = false;
    inputStory: string = this.emptyStory;
    changeStory: string = this.emptyStory;
    savedStoryOnChange = this.emptyStory;
    
    toggleFocusAddInput = false;
    toggleFocusChangeInput = false;

    constructor(
        private restProvider: RestProviderService,
        private router: Router) { }

    onAdd() {
        if (this.validateInput(this.inputStory.trim())) {
            this.stories.push(this.inputStory.trim());
            this.inputStory = this.emptyStory;
            this.checkIsAddStoryEnabled();
        }
        this.setFocus();
    }
    
    onDelete(index: number) {
        this.stories.splice(index, 1);
        this.checkIsAddStoryEnabled();
        this.setFocus();
    }

    onChangeStart(index: number) {
        this.savedStoryOnChange = this.stories[index];
        this.changeStory = this.stories[index];
        this.setFocus();
    }

    onChangeComplete(index: number) {
        this.stories.splice(index, 1);
        if (this.validateInput(this.changeStory.trim())) {
            this.savedStoryOnChange = this.emptyStory;
            this.stories.splice(index, 0, this.changeStory.trim());
        } else {
            this.stories.splice(index, 0, this.savedStoryOnChange);
        }
        this.setFocus();
    }

    onCreateSprint() {
        if (this.isAddStoryEnabled) {
            this.restProvider.createSprint(this.stories)
                .subscribe((id: string) => {
                    this.navigateToSprintCreationResult(id);
                });
        }
    }

    navigateToSprintCreationResult(id: string) {
        this.router.navigate(["/sprint-creation-summary"], { queryParams: { sprintId: id } });
    }

    checkIsAddStoryEnabled() {
        this.isAddStoryEnabled = this.stories.length < 2 ? false : true;
    }

    validateInput(input: string) {
        return (input && !this.stories.some(story => story == input))
    }

    setFocus() {
        if (this.savedStoryOnChange == this.emptyStory) {
            this.toggleFocusAddInput = !this.toggleFocusAddInput;
        } else {
            this.toggleFocusChangeInput = !this.toggleFocusChangeInput;
        }
    }
}