import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { UpdateState } from '../components/UpdateState';

export default {
    title: 'Components/UpdateState',
    component: UpdateState
} as Meta;

const Template = createTemplate(UpdateState);

export const Default = createStory(Template, {
    states: [
        {
            id: 1,
            name: 'Create',
            description: 'The parcel has been created.'
        }
    ]
});
