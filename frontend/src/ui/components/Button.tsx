import classNames from 'classnames';
import { FC, MouseEventHandler } from 'react';

export enum ButtonStyle {
    Red = 'red'
}

export interface BaseButtonProps {
    selected?: boolean;
    activated?: boolean;
    disabled?: boolean;
    fullWidth?: boolean;
    small?: boolean;
    style?: ButtonStyle;
    border?: boolean;
    onClick?: MouseEventHandler<HTMLButtonElement>;
}

function baseButtonClasses(props: BaseButtonProps) {
    return classNames('c-button', {
        'c-button--color-red': props.style == ButtonStyle.Red,
        'c-button--border': props.border,
        'c-button--selected': props.selected,
        'c-button--activated': props.activated,
        'c-button--size-full-width': props.fullWidth,
        'c-button--size-small': props.small
    });
}

export interface ButtonProps extends BaseButtonProps {
    text: string;
    icon?: JSX.Element;
    iconRight?: boolean;
}

export const Button: FC<ButtonProps> = props => {
    const { icon, iconRight, disabled, text, onClick } = props;
    const classes = classNames(baseButtonClasses(props), {
        'c-button--icon-right': iconRight
    });
    return (
        <button className={classes} onClick={onClick} disabled={disabled}>
            {icon}
            {text && <span>{text}</span>}
        </button>
    );
};

export interface IconButtonProps extends BaseButtonProps {
    icon: JSX.Element;
}

export const IconButton: FC<IconButtonProps> = props => {
    const { icon, disabled, onClick } = props;
    const classes = classNames(baseButtonClasses(props), 'c-button--icon');
    return (
        <button className={classes} onClick={onClick} disabled={disabled}>
            {icon}
        </button>
    );
};
