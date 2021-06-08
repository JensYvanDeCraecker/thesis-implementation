import { FC } from 'react';

export interface TextInputProps {
    name?: string;
    value?: string;
    placeholder?: string;
    disabled?: boolean;
    onChange?: (newValue: string) => void;
}

export const TextInput: FC<TextInputProps> = props => {
    const { name, placeholder, value, disabled, onChange } = props;
    return <input className="c-input" name={name} id={name} type="text" value={value} placeholder={placeholder} disabled={disabled} onChange={e => onChange && onChange(e.target.value)} />;
};
