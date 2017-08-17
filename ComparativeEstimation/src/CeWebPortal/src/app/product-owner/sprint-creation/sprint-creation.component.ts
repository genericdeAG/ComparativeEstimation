import { RestProviderService } from './../../rest-provider/rest-provider.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'sprint-creation',
    templateUrl: './sprint-creation.component.html',
    styleUrls: ['./sprint-creation.component.css']
})
export class SprintCreationComponent {
    stories: string[] = [];
    inputStory: string = "";
    changeStory: string = "";
    isAddStoryEnabled = false;
    
    toggleFocusAddInput = false;
    toggleFocusChangeInput = false;

    defaultIndexOnChange = -1;
    indexOnChange = this.defaultIndexOnChange;

    constructor(
        private restProvider: RestProviderService,
        private router: Router) { }

    onAdd() {
        if (this.validateInput(this.inputStory.trim())) {
            this.stories.push(this.inputStory.trim());
            this.inputStory = "";
            this.checkIsAddStoryEnabled();
        }
        this.setFocus();
    }
    
    onDelete(index: number) {
        console.log(this.stories);
        var savedStory = this.stories[this.indexOnChange];
        this.stories.splice(index, 1);
        this.checkIsAddStoryEnabled();
        if (this.indexOnChange != this.defaultIndexOnChange)
            this.indexOnChange = this.stories.indexOf(savedStory);
        this.setFocus();
        console.log(this.stories);
    }

    onChangeStart(index: number) {
        this.indexOnChange = index;
        this.changeStory = this.stories[index];
        this.setFocus();
    }

    onChangeComplete(index: number) {
        var savedStory = this.stories[this.indexOnChange];
        this.stories.splice(index, 1);
        if (this.validateInput(this.changeStory.trim())) {
            this.indexOnChange = this.defaultIndexOnChange;
            this.stories.splice(index, 0, this.changeStory.trim());
        } else {
            this.stories.splice(index, 0, savedStory);
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
        if (this.indexOnChange == this.defaultIndexOnChange) {
            this.toggleFocusAddInput = !this.toggleFocusAddInput;
        } else {
            this.toggleFocusChangeInput = !this.toggleFocusChangeInput;
        }
    }
}