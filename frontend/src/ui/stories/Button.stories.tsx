import { createStory, createTemplate } from '@/utils/storybook';
import { Meta } from '@storybook/react';
import { Button, IconButton } from '../components/Button';
import { AddIcon, DoneIcon } from '@/ui/icons';

export default {
    title: 'Components/Controls/Button',
    component: Button,
    argTypes: {
        icon: {
            options: ['Add', 'Done'],
            mapping: {
                ['Add']: <AddIcon />,
                ['Done']: <DoneIcon />
            }
        }
    }
} as Meta;

const ButtonTemplate = createTemplate(Button);

export const DefaultButton = createStory(
    ButtonTemplate,
    {
        text: 'Default'
    },
    'Default'
);

export const DefaultButtonSelected = createStory(
    ButtonTemplate,
    {
        text: 'Default',
        selected: true
    },
    'Default (selected)'
);

export const DefaultButtonActivated = createStory(
    ButtonTemplate,
    {
        text: 'Default',
        activated: true
    },
    'Default (activated)'
);

export const DefaultButtonIcon = createStory(
    ButtonTemplate,
    {
        text: 'Default',
        icon: <AddIcon />
    },
    'Default (with icon)'
);

export const DefaultButtonSmall = createStory(
    ButtonTemplate,
    {
        text: 'Default',
        small: true
    },
    'Default (small)'
);

const IconButtonTemplate = createTemplate(IconButton);

export const DefaultIconButton = createStory(
    IconButtonTemplate,
    {
        icon: <AddIcon />
    },
    'Default icon'
);

export const DefaultIconButtonSmall = createStory(
    IconButtonTemplate,
    {
        icon: <AddIcon />,
        small: true
    },
    'Default icon (small)'
);
