import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { TextInput } from '../components/TextInput';

export default {
    title: 'Components/Controls/TextInput',
    component: TextInput
} as Meta;

const Template = createTemplate(TextInput);

export const Default = createStory(Template, {});

export const Disabled = createStory(Template, {
    disabled: true
});
