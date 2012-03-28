---
layout: default
---

# History

## Why CropImage and WebCropImage split

For the ethical developer, permanently branching a project is a last resort. You exhaust every other alternative first, because even if the license *permits* branching, it's not really a *nice* thing to do. 

When I <del>updated</del> rewrote WebCropImage.NET **at Cem Sisman's request**, I didn't expect that I would be releasing it myself. But after weeks, then a month passed, and he still hadn't bothered to reply to a **single one** of my 7 e-mails about the completed revision, I posted it on the Discussion page for other people to look at. Cem promptly deleted the thread and send me a one-line e-mail: "I appricate your work on this but placing such a discussion is not acceptable, you should have asked my permission." Oh goodness... I guess I should have sent an eighth e-mail first? When I see your Skype and Google Chat status is online every day, but you don't acknowledge my messages, I am *eventually* going to give up on you. Jan 22 was the first and last day I heard from him, and as far as I know, he never bothered to look at the revision he requested.

I can't emphasize how unprofessional it is to ask an open-source contributor to perform work, and then ignore them. If you solicit help, at least reply to their e-mails. It's OK to say "I'm busy this week, I'll look at it around \[Insert Date Here\]." But coldly hitting the Archive button on source code contributions is a sure way to kill the community around your project. It's stupid to do that to *unsolicited* contributions. If you want to grow a community, at least *pretend* to care, so others will keep things rolling. Milking for donations while you fail to handle merge duties is terribly poor form.

## How I got involved

I first found the WebCropImage project in early 2011. I helped out with support for a few months, made a small donation to the project, and presented Mr. Sisman with an intimidating list of memory leaks and bugs, in the hope that they would get fixed, in order that the project would become stable enough for production use. I even offered to let him use the [imageresizing.net](http://imageresizing.net) library to perform the cropping and image encoding. The existing image processing code was in such bad shape that it needed replacement, not patching. 

Another fundamental flaw was that WebCropImage couldn't handle large images. Even a compact camera uses 8-12 megapixels nowadays - significantly larger than your browser window. The ImageResizing.net project offered built-in dynamic resizing, which would enable WebCropImage to work with *real* images.

We had a few Skype calls about the development in August, and we seemed to agree on the changes that were needed. Over the next few months I sent him patches for various bugs and a new preview system for JCrop. By late November it became clear over that Cem wasn't going to be working on the project anytime soon, despite his promises of renewed focus on open-source and his move to freelancing. 

So on Dec. 5th, I offered to help, and asked to be made a project developer on the CodePlex.com site. He agreed (to quote) "yes I can add you as a developer. that would be great to have you".

After that, I started revising WebCropImage with the changes we had discussed. On December 20th, I shared my new code repository with him via GitHub (since the old SVN repository was corrupt), and had a beta release ready on the 22nd. Unfortunately, at that point, he was so incredibly busy, that he lacked the time to even reply "I'm busy" to any of my e-mails. 