import { FC, useState } from 'react';
import { Button, ButtonStyle } from './Button';
import { TextInput } from './TextInput';

export interface FindParcelProps {
    onSearch?: (parcelId: string) => void;
}

export const FindParcel: FC<FindParcelProps> = props => {
    const { onSearch } = props;

    const [parcelId, setParcelId] = useState('');

    const handleClick = () => {
        if (!parcelId) return;
        if (onSearch) onSearch(parcelId);
        setParcelId('');
    };

    return (
        <div className="c-find-parcel">
            <TextInput value={parcelId} onChange={setParcelId} />
            <Button text="Search" onClick={handleClick} style={ButtonStyle.Red} disabled={!parcelId} />
        </div>
    );
};
