import { FC } from 'react';

export interface SelectProps {
    name?: string;
    items?: { name: string; value: string }[];
    value?: string;
    disabled?: boolean;
    onChange?: (newValue: string) => void;
}

export const Select: FC<SelectProps> = props => {
    const { name, items, value = '', disabled, onChange } = props;
    return (
        <select className="c-select" name={name} id={name} value={value} disabled={disabled} onChange={e => onChange && onChange(e.target.value)}>
            <option value="" disabled>
                Select...
            </option>
            {items?.map(item => (
                <option key={item.value} value={item.value}>{item.name}</option>
            ))}
        </select>
    );
};
