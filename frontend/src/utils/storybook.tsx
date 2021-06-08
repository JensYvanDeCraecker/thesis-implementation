import { Story } from '@storybook/react';
import { FC } from 'react';

export function createStory<Args>(template: Story<Args>, args?: Partial<Args>, storyName?: string) {
    const story = template.bind({});
    story.args = args;
    story.storyName = storyName;
    return story;
}

export function createTemplate<Args>(Component: FC<Args>): Story<Args> {
    return args => <Component {...args} />;
}
