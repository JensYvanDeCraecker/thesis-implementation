declare module '*.svg' {
    import { FC, ReactElement, SVGProps } from 'react';
    const content: FC<SVGProps>;
    export default content;
}
